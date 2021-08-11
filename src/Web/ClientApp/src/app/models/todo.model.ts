export class TodoModel {
    id: number;
    name: string;
    isDone: boolean;

    constructor(obj: TodoModel) { if (obj) { Object.assign(this, obj); } }
}
