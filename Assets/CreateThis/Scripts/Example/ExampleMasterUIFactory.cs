﻿using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
#endif
using CreateThis.Unity;
using CreateThis.Factory;
using CreateThis.Factory.VR.UI;
using CreateThis.Factory.VR.UI.File;
using CreateThis.VR.UI;
using CreateThis.VR.UI.File;
using CreateThis.VR.UI.Panel;
using CreateThis.VR.UI.Controller;

namespace CreateThis.Example {
    public class ExampleMasterUIFactory : BaseFactory {
        public GameObject parent;
        public ExampleSkyboxManager skyboxManager;
        public TouchPadMenuController touchPadMenuController;

        private GameObject disposable;
        private GameObject keyboardInstance;
        private Keyboard keyboard;
        private GameObject fileSaveAsInstance;
        private FileSaveAs fileSaveAs;
        private GameObject fileOpenInstance;
        private FileOpen fileOpen;
        private GameObject toolsInstance;

        private GameObject CreateKeyboard() {
            KeyboardFactory factory = Undoable.AddComponent<KeyboardFactory>(disposable);
            factory.parent = parent;
            GameObject panel = factory.Generate();
            keyboardInstance = panel;
            keyboard = keyboardInstance.GetComponent<Keyboard>();
            return panel;
        }

        private GameObject CreateFileSaveAs() {
            FileSaveAsFactory factory = Undoable.AddComponent<FileSaveAsFactory>(disposable);
            factory.parent = parent;
            factory.keyboard = keyboard;
            GameObject panel = factory.Generate();
            fileSaveAsInstance = panel;
            fileSaveAs = fileSaveAsInstance.transform.Find("DrivesPanel").GetComponent<FileSaveAs>();
            return panel;
        }

        private GameObject CreateFileOpen() {
            FileOpenFactory factory = Undoable.AddComponent<FileOpenFactory>(disposable);
            factory.parent = parent;
            GameObject panel = factory.Generate();
            fileOpenInstance = panel;
            fileOpen = fileOpenInstance.transform.Find("DrivesPanel").GetComponent<FileOpen>();
            return panel;
        }

        private GameObject CreateToolsPanel() {
            ToolsExamplePanelFactory factory = Undoable.AddComponent<ToolsExamplePanelFactory>(disposable);
            factory.parent = parent;
            factory.fileOpen = fileOpen;
            factory.fileSaveAs = fileSaveAs;
            factory.skyboxManager = skyboxManager;
            GameObject panel = factory.Generate();
            toolsInstance = panel;

            var touchPadButtons = touchPadMenuController.touchPadButtons;
            for (int i=0; i < touchPadButtons[0].onSelected.GetPersistentEventCount(); i++) {
                UnityEventTools.RemovePersistentListener(touchPadButtons[0].onSelected, 0);
            }
            UnityEventTools.AddPersistentListener(touchPadButtons[0].onSelected, toolsInstance.GetComponent<StandardPanel>().ToggleVisible);
            touchPadMenuController.touchPadButtons = touchPadButtons;

            return panel;
        }

        private void CreateDisposable() {
            if (disposable) return;
            disposable = EmptyChild(parent, "disposable");
        }

        private void CleanParent() {
            var children = new List<GameObject>();
            foreach (Transform child in parent.transform) children.Add(child.gameObject);
            children.ForEach(child => Undo.DestroyObjectImmediate(child));
        }

        public override GameObject Generate() {
            base.Generate();

#if UNITY_EDITOR
            Undo.SetCurrentGroupName("MasterFactory Generate");
            int group = Undo.GetCurrentGroup();

            Undo.RegisterCompleteObjectUndo(this, "MasterFactory state");
#endif
            CleanParent();

            CreateDisposable();
            CreateKeyboard();
            CreateFileSaveAs();
            CreateFileOpen();
            CreateToolsPanel();

#if UNITY_EDITOR
            Undo.DestroyObjectImmediate(disposable);
            Undo.CollapseUndoOperations(group);
#else
            Destroy(disposable);
#endif
            return parent;
        }
    }
}