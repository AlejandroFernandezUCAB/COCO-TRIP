import {Category,CategoryLogger,CategoryServiceFactory,CategoryConfiguration,LogLevel} from "typescript-logging";
//****************************************************************************************************//
//*****************************************CONFIG LOGS MODULO 6***************************************//
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
 * Configuracion de los logs
 */

CategoryServiceFactory.setDefaultConfiguration(new CategoryConfiguration(LogLevel.Info));

export const catService = new Category("service");
export const catProd = new Category("product", catService);
export const catErr = new Category("Error"); 