export const apis = {
    todos: 'todos',

    todosStatusUpdate: (id: number) => {
        return `todos/${id}/update-status`;
    }
};
