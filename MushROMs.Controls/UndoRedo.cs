using System;
using System.Collections.Generic;

namespace MushROMs.Controls
{
    public class UndoRedo<T>
    {
        #region Events
        public event EventHandler UndoDataAdded;
        public event EventHandler RedoDataAdded;

        public event EventHandler Undo;
        public event EventHandler Redo;

        public event EventHandler LastUndo;
        public event EventHandler LastRedo;
        #endregion

        #region Variables
        private int saveIndex;
        private int historyIndex;
        private bool forceUnsaved;
        private List<T> undo;
        private List<T> redo;
        #endregion

        #region Accessors
        public int SaveIndex
        {
            get { return this.saveIndex; }
        }

        public int HistoryIndex
        {
            get { return this.historyIndex; }
        }

        public bool ForceUnsaved
        {
            get { return this.forceUnsaved; }
            set { this.forceUnsaved = value; }
        }

        public bool Unsaved
        {
            get { return this.historyIndex != this.saveIndex || this.forceUnsaved; }
        }

        public T CurrentUndoData
        {
            get { return this.undo[this.historyIndex]; }
        }

        public T CurrentRedoData
        {
            get { return this.redo[this.historyIndex]; }
        }
        #endregion

        #region Initializers
        public UndoRedo()
        {
            Reset();
        }

        public UndoRedo(bool forceUnsaved)
        {
            Reset(forceUnsaved);
        }
        #endregion

        #region Methods
        public void Reset()
        {
            Reset(false);
        }

        public void Reset(bool forceUnsaved)
        {
            this.saveIndex =
            this.historyIndex = 0;

            this.undo = new List<T>();
            this.redo = new List<T>();

            this.forceUnsaved = forceUnsaved;
        }

        public void SetSaveIndex()
        {
            this.saveIndex = this.historyIndex;
            this.forceUnsaved = false;
        }

        public void AddUndoData(T data)
        {
            if (this.historyIndex < this.undo.Count)
            {
                this.undo.RemoveRange(this.historyIndex, this.undo.Count - this.historyIndex);
                this.redo.RemoveRange(this.historyIndex, this.redo.Count - this.historyIndex);
            }

            this.undo.Add(data);

            OnUndoDataAdded(EventArgs.Empty);
        }

        public void AddRedoData(T data)
        {
            this.redo.Add(data);
            this.historyIndex++;

            OnRedoDataAdded(EventArgs.Empty);
        }

        public void UndoChange()
        {
            if (this.historyIndex == 0)
                return;

            this.historyIndex--;
            OnUndo(EventArgs.Empty);

            if (this.historyIndex == 0)
                OnLastUndo(EventArgs.Empty);
        }

        public void RedoChange()
        {
            if (this.historyIndex == this.redo.Count)
                return;

            OnRedo(EventArgs.Empty);
            this.historyIndex++;

            if (this.historyIndex == this.redo.Count)
                OnLastRedo(EventArgs.Empty);
        }
        #endregion

        #region Event calls
        protected virtual void OnUndoDataAdded(EventArgs e)
        {
            if (UndoDataAdded != null)
                UndoDataAdded(this, e);
        }

        protected virtual void OnRedoDataAdded(EventArgs e)
        {
            if (RedoDataAdded != null)
                RedoDataAdded(this, e);
        }

        protected virtual void OnUndo(EventArgs e)
        {
            if (Undo != null)
                Undo(this, e);
        }

        protected virtual void OnRedo(EventArgs e)
        {
            if (Redo != null)
                Redo(this, e);
        }

        protected virtual void OnLastUndo(EventArgs e)
        {
            if (LastUndo != null)
                LastUndo(this, e);
        }

        protected virtual void OnLastRedo(EventArgs e)
        {
            if (LastRedo != null)
                LastRedo(this, e);
        }
        #endregion
    }
}