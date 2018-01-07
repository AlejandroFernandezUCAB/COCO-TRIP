import { Entidad } from './entidad';
//****************************************************************************************************//
//*****************************************CLASE MENSAJE MODULO 6*************************************//
//****************************************************************************************************//

/**
 * Autores:
 * Mariangel Perez
 * Oswaldo Lopez
 * Aquiles Pulido
 */

/**
 * Descripcion de la clase:
 * 
 * Clase mensaje
 */
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

    /**
     * Obtiene el mensaje
     */
     get getMensaje():string {
        return this._mensaje;
    }

    /**
     * Establece el mensaje
     */
    set setMensaje(mensaje:string) {
        this._mensaje = mensaje;
    }
    
    /**
     * Obtiene el usuario (Emisor)
     */
    get getUsuario():string {
        return this._usuario;
    }

    /**
     * Establece el usuario (Emisor)
     */
    set setUsuario(usuario:string) {
        this._usuario = usuario;
    }

    /**
     * Obtiene el amigo (Receptor)
     */
    get getAmigo():string {
        return this._amigo;
    }

    /**
     * Establece el amigo (Receptor)
     */
    set setAmigo(amigo:string) {
        this._amigo = amigo;
    }

    /**
     * Obtiene el identificador del grupo
     */
    get getidGrupo():number {
        return this._idGrupo;
    }

    /**
     * Establece el identificador del grupo
     */
    set setidGrupo(id:number) {
        this._idGrupo = id;
    }
    
    /**
     * Obtiene la fecha del mensaje
     */
    get getFecha():any {
        return this._fecha;
    }

    /**
     * Establece la fecha del mensaje
     */
    set setFecha(fecha:any) {
        this._fecha = fecha;
    }

    /**
     * Obtiene la hora del mensaje
     */
    get getHora():any {
        return this._hora;
    }

    /**
     * Establece la hora del mensaje
     */
    set setHora(hora:any) {
        this._hora = hora;
        
    }

    /**
     * Obtiene si el mensaje fue modificado o no
     */
    get getModificado():boolean {
        return this._modificado;
    }

    /**
     * Establece si el mensaje es modificado
     */
    set setModificado(modificado:boolean) {
        this._modificado = modificado;
        
    }
    
}