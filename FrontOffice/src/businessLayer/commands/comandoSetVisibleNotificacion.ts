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
 * Solicita al servicio webset visible notificaciones
 */
@Injectable()
export class ComandoSetVisibleNotificacion extends Comando
{
    public constructor(idUsuario:number,idIt:number,visible:boolean)
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