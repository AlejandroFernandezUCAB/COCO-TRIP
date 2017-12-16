import { Entidad } from './entidad';

export class Mensaje extends Entidad {
    private _mensaje: string;
    private _usuario: string;
    private _amigo:string;
    
    constructor(mensaje : string , usuario : string , amigo : string ) {
        super();
        this._mensaje = mensaje;
        this._usuario = usuario;
        this._amigo = amigo;
        
    }

     get getMensaje():string {
        return this._mensaje;
    }
    set setMensaje(mensaje:string) {
        this._mensaje = mensaje;
    }
    

    get getUsuario():string {
        return this._usuario;
    }
    set setUsuario(usuario:string) {
        this._usuario = usuario;
    }


    get getAmigo():string {
        return this._amigo;
    }
    set setAmigo(amigo:string) {
        this._amigo = amigo;
    }
    
    
}