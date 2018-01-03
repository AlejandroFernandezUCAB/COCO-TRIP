import { Comando } from "./comando";
import { Injectable } from "@angular/core";
import { RestapiService } from "../../providers/restapi-service/restapi-service";
import { Itinerario } from "../../dataAccessLayer/domain/itinerario";

/**
 * Autores:
 * Jorge Marin
 */

//*************************************************************************************//
//*************************************MODULO 5****************************************//
//*************************************************************************************//

/**
 * Solicita al servicio web modificar itinerario
 */
@Injectable()
export class ComandoModificarItinerario extends Comando
{
    public constructor(itinerario:Itinerario)
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