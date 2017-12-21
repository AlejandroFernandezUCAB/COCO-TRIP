export class Entidad{
    protected _id: String;

    constructor(){

    }

    set setId(id : String) {
        this._id = id;
    }

    get getId():String {
        return this._id;
    }
}