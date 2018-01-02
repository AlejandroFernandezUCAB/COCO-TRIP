import { Comando } from "./comando";
import { Injectable } from "@angular/core";
import { RestapiService } from "../../providers/restapi-service/restapi-service";
import { DateTime } from "ionic-angular";

/**
 * Autores:
 * Jorge Marin
 */

//*************************************************************************************//
//*************************************MODULO 5****************************************//
//*************************************************************************************//

/**
 * Solicita al servicio web agregar item al itinerario
 */
@Injectable()
export class ComandoAgregarItemItinerario extends Comando
{
    public constructor(tipo:string,idIt:number,itemId:number,fechai:DateTime,
        fechaf:DateTime)
    {
        super();
    }
    public execute(): void {
        throw new Error("Method not implemented.");
    }
    public return() {
        throw new Error("Method not implemented.");
    }
    public isSuccess(): boolean {
        throw new Error("Method not implemented.");
    }
 
}