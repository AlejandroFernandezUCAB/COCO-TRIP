
//****************************************************************************************************//
//*****************************************CLASE MENSAJE MODULO 3*************************************//
//****************************************************************************************************//

export class Texto
{
    public static readonly CARGANDO : string = 'Por favor espere';
    
    //General
    public static readonly TITULO : string = 'Por favor, confirmar';
    public static readonly CANCELAR : string = 'Cancelar';
    public static readonly ACEPTAR : string = 'Aceptar';
    public static readonly REQUERIDO : string = 'Este campo es requerido';

    //EliminarAmigo
    public static readonly MENSAJE_ELIMINAR_AMIGO : string = 'Deseas borrar a: ';
    public static readonly EXITO_ELIMINAR_AMIGO : string = 'Eliminado amigo exitosamente';

    //ConfirmarAmigo
    public static readonly TITULO_CONFIRMAR : string = 'Agregar';
    public static readonly MENSJAE_CONFIRMAR : string = '¿Deseas agregar a esta persona como amigo?';
    public static readonly SI_CONFIRMAR : string = 'Si';
    public static readonly EXITO_CONFIRMAR : string = 'Peticion de amistad enviada exitosamente';
    public static readonly EXITO_CORREO : string = 'Se ha enviado una notificacion al usuario';

    //AgregarGrupo
    public static readonly EXITO_AGREGAR_GRUPO : string = 'Agregado grupo exitosamente';

    //EliminarGrupo
    public static readonly MENSAJE_ELIMINAR_GRUPO : string = 'Borrar grupo';
    public static readonly EXITO_ELIMINAR_GRUPO : string = 'Saliste del grupo';

    //AgregarIntegrante
    public static readonly MENSAJE_AGREGAR_INTEGRANTE : string = 'Deseas agregar a: ';
    public static readonly EXITO_AGREGAR_INTEGRANTE : string = 'Agregado miembro exitosamente';

    //EliminarIntegrante
    public static readonly MENSAJE_ELIMINAR_INTEGRANTE : string = 'Deseas borrar a: ';
    public static readonly EXITO_ELIMINAR_INTEGRANTE : string = 'Eliminado miembro exitosamente';

    //AlertaIntegrante
    public static readonly NO_EDITAR_ALERTA_INTEGRANTE : string = 'No eres el lider del grupo';
    public static readonly OK_ALERTA_INTEGRANTE : string = 'Esta bien';    

    //Mensajes Toast
    public static readonly ERROR : string = 'Algo ha salido mal :(';
    public static readonly ACEPTAR_PETICION : string = 'Peticion de amistad aceptada';
    public static readonly RECHAZAR_PETICION : string = 'Peticion de amistad rechazada';
    public static readonly PETICION_ELIMINADA : string = 'Peticion eliminada';
    public static readonly GRUPO_EXITOSO : string = 'Cambios guardados exitosamente';
    public static readonly MODIFICAR_EXITOSO : string = 'Modificado exitosamente';
    
}