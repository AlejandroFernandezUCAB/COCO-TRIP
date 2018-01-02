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
 * Solicita al servicio web consultar actividades
 */
@Injectable()
export class ComandoConsultarActividades extends Comando
{
    public constructor(nombre:string)
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