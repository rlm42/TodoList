export class TodoStatusModel {

    id: number = null;

    isDone: boolean = null;

    constructor(obj: TodoStatusModel = null) { if (obj) { Object.assign(this, obj); } }
}