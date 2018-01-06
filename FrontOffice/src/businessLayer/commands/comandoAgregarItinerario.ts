import { Comando } from "./comando";
import { Injectable } from "@angular/core";
import { RestapiService } from "../../providers/restapi-service/restapi-service";
import { FabricaDAO } from "../factory/fabricaDao";
import { FabricaComando } from "../factory/fabricaComando";
import { Itinerario } from "../../dataAccessLayer/domain/itinerario";
import { FabricaEntidad } from "../../dataAccessLayer/factory/fabricaEntidad";
import { HttpCProvider } from "../../providers/http-c/http-c";
import { catProd, catErr } from "../../logs/config";

/**
 * Autores:
 * Jorge Marin
 */

//*************************************************************************************//
//*************************************MODULO 5****************************************//
//*************************************************************************************//

/**
 * Solicita al servicio web agregar el nuevo grupo
 */
@Injectable()
export class ComandoAgregarItinerario extends Comando
{
private _itinerario : Itinerario;
    public constructor(private peticion:HttpCProvider)
    {
        super();
        this._itinerario = FabricaEntidad.crearItinerario();
    }
    set Nombre(nombre:string)
    {
        this._itinerario.Nombre = nombre;
    }
    set IdUsuario(id:number)
    {
        this._itinerario.IdUsuario = id;
    }
    public execute() {
        return this.peticion.agregarItinerario(this._itinerario)
        .then(datos => 
        {
            catProd.info('Agregar Itinerario exitoso. Datos: ' + datos);
            return true;
        }
        , error =>
        {
            catErr.info('Fallo de Agregando un itinerario. Datos: ' + error);
            return false;
        })
    }
    public return() {
        throw new Error("Method not implemented.");
    }
    public isSuccess(): boolean {
        throw new Error("Method not implemented.");
    }
 
}