import { Comando } from "./comando";
import { Injectable } from "@angular/core";
import { RestapiService } from "../../providers/restapi-service/restapi-service";

/**
 * Autores:
 * Jorge Marin
 */

//*************************************************************************************//
//*************************************MODULO 5****************************************//
//*************************************************************************************//

/**
 * Solicita al servicio web ver item
 */
@Injectable()
export class ComandoVerItem extends Comando
{
    public constructor(idItem :number,tipo:string)
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