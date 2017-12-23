import { Entidad } from './entidad';

export class Mensaje extends Entidad {
    private _mensaje: string;
    private _usuario: string;
    private _amigo:string;
    private _idGrupo:number;
    private _fecha:any;
    private _hora:any;
    private _modificado:boolean;

    
    constructor(mensaje : string , usuario : string , amigo : string, idGrupo:number, fecha:any, hora:any,modificado:boolean ) {
        super();
        this._mensaje = mensaje;
        this._usuario = usuario;
        this._amigo = amigo;
        this._idGrupo=idGrupo;
        this._fecha=fecha;
        this._hora=hora;
        this._modificado=modificado;

        
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
    
    get getFecha():any {
        return this._fecha;
    }
    set setFecha(fecha:any) {
        this._fecha = fecha;
    }

    get getHora():any {
        return this._hora;
    }
    set setHora(hora:any) {
        this._hora = hora;
        
    }

    get getModificado():boolean {
        return this._modificado;
    }
    set setModificado(modificado:boolean) {
        this._modificado = modificado;
        
    }
    
}