import { Entidad } from './entidad';

export class Mensaje extends Entidad {
    private _mensaje: string;
    private _usuario: string;
    private _amigo:string;
    private _idGrupo:number;
    
    constructor(mensaje : string , usuario : string , amigo : string, idGrupo:number ) {
        super();
        this._mensaje = mensaje;
        this._usuario = usuario;
        this._amigo = amigo;
        this._idGrupo=idGrupo;

        
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

    get getidGrupo():number {
        return this._idGrupo;
    }
    set setidGrupo(id:number) {
        this._idGrupo = id;
    }
    
    
}