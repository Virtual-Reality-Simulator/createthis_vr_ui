﻿using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using CreateThis.Factory.VR.UI.Button;
using CreateThis.Factory.VR.UI.Container;
using CreateThis.VR.UI;
using CreateThis.VR.UI.Panel;
using CreateThis.VR.UI.Container;

namespace CreateThis.Factory.VR.UI {
    public class KeyboardFactory : KeyboardKey {
        public GameObject parent;
        public GameObject buttonBody;
        public Material buttonMaterial;
        public Material panelMaterial;
        public Material highlight;
        public Material outline;
        public AudioClip buttonClickDown;
        public AudioClip buttonClickUp;
        public int fontSize;
        public Color fontColor;
        public float labelZ;
        public float buttonZ;
        public Vector3 bodyScale;
        public Vector3 labelScale;
        public float padding;
        public float spacing;
        public float buttonPadding;
        public float keyMinWidth;
        public float keyCharacterSize;
        public float numLockCharacterSize;
        public float spaceMinWidth;
        public float returnMinWidth;
        public float spacerWidth;
        public float modeKeyMinWidth;
        public float wideKeyMinWidth;

        protected Keyboard keyboard;
        protected GameObject keyboardInstance;
        protected GameObject panelLowerCase;
        protected GameObject panelUpperCase;
        protected GameObject panelNumber;
        protected GameObject panelSymbol;
        private GameObject disposable;

        protected void SetKeyboardButtonValues(KeyboardButtonFactory factory, GameObject parent) {
            factory.parent = parent;
            factory.buttonBody = buttonBody;
            factory.material = buttonMaterial;
            factory.highlight = highlight;
            factory.outline = outline;
            factory.buttonClickDown = buttonClickDown;
            factory.buttonClickUp = buttonClickUp;
            factory.alignment = TextAlignment.Center;
            factory.fontSize = fontSize;
            factory.fontColor = fontColor;
            factory.labelZ = labelZ;
            factory.bodyScale = bodyScale;
            factory.labelScale = labelScale;
            factory.keyboard = keyboard;
            factory.minWidth = keyMinWidth;
            factory.padding = buttonPadding;
            factory.characterSize = keyCharacterSize;
        }

        protected void SetKeyboardButtonPosition(GameObject button) {
            Vector3 localPosition = button.transform.localPosition;
            localPosition.z = buttonZ;
            button.transform.localPosition = localPosition;
        }

        protected GameObject GenerateKeyboardButtonAndSetPosition(KeyboardButtonFactory factory) {
            GameObject button = factory.Generate();
            SetKeyboardButtonPosition(button);
            return button;
        }

        protected GameObject KeyboardButton(GameObject parent, string buttonText, float minWidth = -1) {
            KeyboardMomentaryKeyButtonFactory factory = SafeAddComponent<KeyboardMomentaryKeyButtonFactory>(disposable);
            SetKeyboardButtonValues(factory, parent);
            factory.buttonText = buttonText;
            factory.value = buttonText;
            if (minWidth != -1) factory.minWidth = minWidth;
            return GenerateKeyboardButtonAndSetPosition(factory);
        }

        protected GameObject KeyboardSpaceButton(GameObject parent, string buttonText) {
            KeyboardMomentaryKeyButtonFactory factory = SafeAddComponent<KeyboardMomentaryKeyButtonFactory>(disposable);
            SetKeyboardButtonValues(factory, parent);
            factory.buttonText = buttonText;
            factory.value = " ";
            factory.minWidth = spaceMinWidth;
            return GenerateKeyboardButtonAndSetPosition(factory);
        }

        protected GameObject KeyboardShiftLockButton(GameObject parent, string buttonText, bool on) {
            KeyboardShiftLockButtonFactory factory = SafeAddComponent<KeyboardShiftLockButtonFactory>(disposable);
            SetKeyboardButtonValues(factory, parent);
            factory.buttonText = buttonText;
            factory.on = on;
            factory.minWidth = modeKeyMinWidth;
            return GenerateKeyboardButtonAndSetPosition(factory);
        }

        protected GameObject KeyboardABCButton(GameObject parent, string buttonText) {
            KeyboardShiftLockButtonFactory factory = SafeAddComponent<KeyboardShiftLockButtonFactory>(disposable);
            SetKeyboardButtonValues(factory, parent);
            factory.buttonText = buttonText;
            factory.on = false;
            factory.characterSize = numLockCharacterSize;
            factory.minWidth = modeKeyMinWidth;
            return GenerateKeyboardButtonAndSetPosition(factory);
        }

        protected GameObject KeyboardNumLockButton(GameObject parent, string buttonText) {
            KeyboardNumLockButtonFactory factory = SafeAddComponent<KeyboardNumLockButtonFactory>(disposable);
            SetKeyboardButtonValues(factory, parent);
            factory.buttonText = buttonText;
            factory.characterSize = numLockCharacterSize;
            factory.minWidth = modeKeyMinWidth;
            return GenerateKeyboardButtonAndSetPosition(factory);
        }

        protected GameObject KeyboardReturnButton(GameObject parent, string buttonText) {
            KeyboardReturnButtonFactory factory = SafeAddComponent<KeyboardReturnButtonFactory>(disposable);
            SetKeyboardButtonValues(factory, parent);
            factory.buttonText = buttonText;
            factory.characterSize = keyCharacterSize;
            factory.minWidth = returnMinWidth;
            return GenerateKeyboardButtonAndSetPosition(factory);
        }

        protected GameObject KeyboardSymbolButton(GameObject parent, string buttonText) {
            KeyboardSymbolButtonFactory factory = SafeAddComponent<KeyboardSymbolButtonFactory>(disposable);
            SetKeyboardButtonValues(factory, parent);
            factory.buttonText = buttonText;
            factory.characterSize = numLockCharacterSize;
            factory.minWidth = modeKeyMinWidth;
            return GenerateKeyboardButtonAndSetPosition(factory);
        }

        protected GameObject KeyboardBackspaceButton(GameObject parent, string buttonText) {
            KeyboardBackspaceButtonFactory factory = SafeAddComponent<KeyboardBackspaceButtonFactory>(disposable);
            SetKeyboardButtonValues(factory, parent);
            factory.buttonText = buttonText;
            return GenerateKeyboardButtonAndSetPosition(factory);
        }

        protected GameObject ButtonSpacer(GameObject parent, float width) {
            ButtonSpacerFactory factory = SafeAddComponent<ButtonSpacerFactory>(disposable);
            factory.parent = parent;
            Vector3 size = bodyScale;
            size.x = width;
            factory.size = size;
            GameObject button = factory.Generate();
            SetKeyboardButtonPosition(button);
            return button;
        }

        protected GameObject Row(GameObject parent, string name = null, TextAlignment alignment = TextAlignment.Center) {
            RowContainerFactory factory = SafeAddComponent<RowContainerFactory>(disposable);
            factory.containerName = name;
            factory.parent = parent;
            factory.padding = padding;
            factory.spacing = spacing;
            factory.alignment = alignment;
            return factory.Generate();
        }

        protected GameObject Column(GameObject parent) {
            ColumnContainerFactory factory = SafeAddComponent<ColumnContainerFactory>(disposable);
            factory.parent = parent;
            factory.padding = padding;
            factory.spacing = spacing;
            return factory.Generate();
        }

        protected GameObject Panel(GameObject parent, string name) {
            PanelContainerFactory factory = SafeAddComponent<PanelContainerFactory>(disposable);
            factory.parent = parent;
            factory.containerName = name;
            factory.panelBody = buttonBody;
            factory.material = panelMaterial;
            factory.highlight = highlight;
            factory.outline = outline;
            factory.fontColor = fontColor;
            factory.bodyScale = bodyScale;
            return factory.Generate();
        }

        protected GameObject DisplayRow(GameObject parent) {
            GameObject row = Row(parent, "DisplayRow", TextAlignment.Left);
            GameObject label = EmptyChild(row, "Display");
            label.transform.localScale = labelScale;
            Vector3 localPosition = new Vector3(0, 0, labelZ);
            label.transform.localPosition = localPosition;
            TextMesh textMesh = SafeAddComponent<TextMesh>(label);
            textMesh.text = "keyboard display";
            textMesh.fontSize = fontSize;
            textMesh.color = fontColor;
            textMesh.anchor = TextAnchor.MiddleCenter;

            KeyboardLabel keyboardLabel = SafeAddComponent<KeyboardLabel>(label);
            keyboardLabel.keyboard = keyboard;
            keyboardLabel.textMesh = textMesh;

            BoxCollider boxCollider = SafeAddComponent<BoxCollider>(label);

            UpdateBoxColliderFromTextMesh updateBoxCollider = SafeAddComponent<UpdateBoxColliderFromTextMesh>(label);
            updateBoxCollider.textMesh = textMesh;
            updateBoxCollider.boxCollider = boxCollider;

            return row;
        }

        protected void ButtonByKey(GameObject parent, Key key) {
            switch (key.type) {
                case KeyType.Key:
                    KeyboardButton(parent, key.value);
                    break;
                case KeyType.Wide:
                    KeyboardButton(parent, key.value, wideKeyMinWidth);
                    break;
                case KeyType.Space:
                    KeyboardSpaceButton(parent, key.value);
                    break;
                case KeyType.ShiftLock:
                    KeyboardShiftLockButton(parent, key.value, key.on);
                    break;
                case KeyType.ABC:
                    KeyboardABCButton(parent, key.value);
                    break;
                case KeyType.NumLock:
                    KeyboardNumLockButton(parent, key.value);
                    break;
                case KeyType.Symbol:
                    KeyboardSymbolButton(parent, key.value);
                    break;
                case KeyType.Return:
                    KeyboardReturnButton(parent, key.value);
                    break;
                case KeyType.Backspace:
                    KeyboardBackspaceButton(parent, key.value);
                    break;
                case KeyType.Spacer:
                    ButtonSpacer(parent, key.width);
                    break;
                default:
                    Debug.Log("unhandled key type=" + key.type);
                    break;
            }
        }

        protected GameObject ButtonRow(GameObject parent, List<Key> keys) {
            GameObject row = Row(parent);
            foreach (Key key in keys) {
                ButtonByKey(row, key);
            }
            return row;
        }

        private void CreateDisposable(GameObject parent) {
            if (disposable) return;
            disposable = EmptyChild(parent, "disposable");
        }

        protected void CreateKeyboard(GameObject parent) {
            if (keyboardInstance) return;
            keyboardInstance = EmptyChild(parent, "keyboard");

            keyboard = SafeAddComponent<Keyboard>(keyboardInstance);
        }

        protected void PanelLowerCase(GameObject parent) {
            if (panelLowerCase) return;

            GameObject panel = Panel(parent, "PanelLowerCase");
            SafeAddComponent<StandardPanel>(panel);

            GameObject column = Column(panel);

            DisplayRow(column);

            ButtonRow(column, new List<Key> {
                K("q"), K("w"), K("e"), K("r"), K("t"), K("y"), K("u"), K("i"), K("o"), K("p")
            });
            ButtonRow(column, new List<Key> {
                K("a"), K("s"), K("d"), K("f"), K("g"), K("h"), K("j"), K("k"), K("l")
            });
            ButtonRow(column, new List<Key> {
                Key.ShiftLock("⇧", false), Key.Spacer(spacerWidth), K("z"), K("x"), K("c"), K("v"), K("b"), K("n"), K("m"), Key.Spacer(spacerWidth), Key.Backspace("⇦")
            });
            ButtonRow(column, new List<Key> {
                Key.NumLock("123"), Key.Spacer(spacerWidth), Key.Space("space"), Key.Return("return")
            });

            panelLowerCase = panel;
            keyboard.panelLowerCase = panelLowerCase.GetComponent<StandardPanel>();
        }

        protected void PanelUpperCase(GameObject parent) {
            if (panelUpperCase) return;

            GameObject panel = Panel(parent, "PanelUpperCase");
            SafeAddComponent<StandardPanel>(panel);

            GameObject column = Column(panel);

            DisplayRow(column);

            ButtonRow(column, new List<Key> {
                K("Q"), K("W"), K("E"), K("R"), K("T"), K("Y"), K("U"), K("I"), K("O"), K("P")
            });
            ButtonRow(column, new List<Key> {
                K("A"), K("S"), K("D"), K("F"), K("G"), K("H"), K("J"), K("K"), K("L")
            });
            ButtonRow(column, new List<Key> {
                Key.ShiftLock("⇧", true), Key.Spacer(spacerWidth), K("Z"), K("X"), K("C"), K("V"), K("B"), K("N"), K("M"), Key.Spacer(spacerWidth), Key.Backspace("⇦")
            });
            ButtonRow(column, new List<Key> {
                Key.NumLock("123"), Key.Spacer(spacerWidth), Key.Space("space"), Key.Return("return")
            });

            panelUpperCase = panel;
            keyboard.panelUpperCase = panelUpperCase.GetComponent<StandardPanel>();
        }

        protected void PanelNumber(GameObject parent) {
            if (panelNumber) return;

            GameObject panel = Panel(parent, "PanelNumber");
            SafeAddComponent<StandardPanel>(panel);

            GameObject column = Column(panel);

            DisplayRow(column);

            ButtonRow(column, new List<Key> {
                K("1"), K("2"), K("3"), K("4"), K("5"), K("6"), K("7"), K("8"), K("9"), K("0")
            });
            ButtonRow(column, new List<Key> {
                K("-"), K("/"), K(":"), K(";"), K("("), K(")"), K("$"), K("&"), K("@"), K("\"")
            });
            ButtonRow(column, new List<Key> {
                Key.Symbol("#+="), Key.Spacer(spacerWidth), W("."), W(","), W("?"), W("!"), W("'"), Key.Spacer(spacerWidth), Key.Backspace("⇦")
            });
            ButtonRow(column, new List<Key> {
                Key.ABC("ABC"), Key.Spacer(spacerWidth), Key.Space("space"), Key.Return("return")
            });

            panelNumber = panel;
            keyboard.panelNumber = panelNumber.GetComponent<StandardPanel>();
        }

        protected void PanelSymbol(GameObject parent) {
            if (panelSymbol) return;

            GameObject panel = Panel(parent, "PanelSymbol");
            SafeAddComponent<StandardPanel>(panel);

            GameObject column = Column(panel);

            DisplayRow(column);

            ButtonRow(column, new List<Key> {
                K("["), K("]"), K("{"), K("}"), K("#"), K("%"), K("^"), K("*"), K("+"), K("=")
            });
            ButtonRow(column, new List<Key> {
                K("_"), K("\\"), K("|"), K("~"), K("<"), K(">"), K("€"), K("£"), K("¥"), K("•")
            });
            ButtonRow(column, new List<Key> {
                Key.NumLock("123"), Key.Spacer(spacerWidth), W("."), W(","), W("?"), W("!"), W("'"), Key.Spacer(spacerWidth), Key.Backspace("⇦")
            });
            ButtonRow(column, new List<Key> {
                Key.ABC("ABC"), Key.Spacer(spacerWidth), Key.Space("space"), Key.Return("return")
            });

            panelSymbol = panel;
            keyboard.panelSymbol = panelSymbol.GetComponent<StandardPanel>();
        }

        public override GameObject Generate() {
            base.Generate();

#if UNITY_EDITOR
            Undo.SetCurrentGroupName("KeyboardFactory Generate");
            int group = Undo.GetCurrentGroup();

            Undo.RegisterCompleteObjectUndo(this, "KeyboardFactory state");
#endif
            CreateDisposable(parent);
            CreateKeyboard(parent);
            PanelLowerCase(keyboardInstance);
            PanelUpperCase(keyboardInstance);
            PanelNumber(keyboardInstance);
            PanelSymbol(keyboardInstance);

#if UNITY_EDITOR
            Undo.DestroyObjectImmediate(disposable);
            Undo.CollapseUndoOperations(group);
#else
            Destroy(disposable);
#endif
            return keyboardInstance;
        }
    }
}