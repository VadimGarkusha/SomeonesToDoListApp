import './ToDo.css';
import ToDoItem from './ToDoItem';
import React, { useEffect, useState } from "react";
import axios from "axios";

function ToDo() {

    const [state, setState] = useState({
        toDoItems: [],
        newToDo: ""
    });

    const loadAndUpdateToDoItems = (cb) => {
        axios.get('http://localhost:62116/ToDo/GetToDos')
            .then(res => {
                const toDoItems = res && res.data ? res.data : [];

                setState({ ...state, toDoItems: toDoItems });
            }).catch(err => {
                console.error(err);
            }).then(() => {
                if (cb)
                    cb();
            });
    }

    const createNewToDo = () => {
        axios.post('http://localhost:62116/ToDo/CreateToDo', {
            ToDoItem: state.newToDo
        }).then(res => {
            if (res && res.data && !res.ErrorMessage) {
                loadAndUpdateToDoItems(() => { setState(prevState => ({ ...prevState, newToDo: "" })) });
            }
        }).catch(err => {
            console.error(err);
        });
    }

    useEffect(() => {
        loadAndUpdateToDoItems();
    }, []);

    return (
        <div className="todo-container">
            <h1>SomeOnes ToDo List</h1>
            <input value={state.newToDo} onInput={e => { setState({ ...state, newToDo: e.target.value }); }} />
            <button onClick={createNewToDo}>Add new ToDo</button>

            <div>
                {
                    state.toDoItems.map(item =>
                        <ToDoItem key={item.Id} toDoItem={item} loadAndUpdateToDoItems={loadAndUpdateToDoItems} />
                    )
                }
            </div>
        </div>
    );
}

export default ToDo;
