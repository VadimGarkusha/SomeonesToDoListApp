import './ToDoItem.css';
import React, { useState } from "react";
import axios from "axios";

function ToDoItem({ toDoItem, loadAndUpdateToDoItems }) {
    const originalToDoValue = toDoItem.ToDoItem;
    const [state, setState] = useState({
        isEditModeEnabled: false,
        editToDoItem: originalToDoValue
    });

    const toggleEditMode = () => {
        setState({ ...state, isEditModeEnabled: !state.isEditModeEnabled });
    };

    const cancelEditing = () => {
        toggleEditMode();
        setState(prevState => ({ ...prevState, editToDoItem: originalToDoValue }));
    };

    const updateToDoItem = () => {
        axios.put('http://localhost:62116/ToDo/UpdateToDo', {
            ToDoItem: state.editToDoItem,
            Id: toDoItem.Id
        }).then(res => {
            if (res && res.data && !res.ErrorMessage) {
                cancelEditing();
                loadAndUpdateToDoItems();
            }
        }).catch(err => {
            console.error(err);
        });
    };

    return (
        <>
            {
                state.isEditModeEnabled ?
                    <div className="todo-item">
                        <input value={state.editToDoItem} onInput={e => { setState({ ...state, editToDoItem: e.target.value }); }} />
                        <button onClick={updateToDoItem}>Update</button>
                        <button onClick={cancelEditing}>Cancel</button>
                    </div>
                    :
                    <div className="todo-item">
                        <span>{originalToDoValue}</span>
                        <button onClick={toggleEditMode}>Edit</button>
                    </div>
            }
        </>
    );
}

export default ToDoItem;
