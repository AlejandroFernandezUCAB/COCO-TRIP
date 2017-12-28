
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
    public static readonly EXITO_ELIMINAR_AMIGO : string = 'Eliminado exitosamente';

    //ConfirmarAmigo
    public static readonly TITULO_CONFIRMAR : string = 'Agregar';
    public static readonly MENSJAE_CONFIRMAR : string = '¿Deseas agregar a esta persona como amigo?';
    public static readonly SI_CONFIRMAR : string = 'Si';

    //AgregarGrupo
    public static readonly EXITO_AGREGAR_GRUPO : string = 'Agregado exitosamente';

    //EliminarGrupo
    public static readonly MENSAJE_ELIMINAR_GRUPO : string = 'Borrar grupo';
    public static readonly EXITO_ELIMINAR_GRUPO : string = 'Salir grupo';

    //AgregarIntegrante
    public static readonly MENSAJE_AGREGAR_INTEGRANTE : string = 'Deseas agregar a: ';
    public static readonly EXITO_AGREGAR_INTEGRANTE : string = 'Agregado exitosamente';

    //EliminarIntegrante
    public static readonly MENSAJE_ELIMINAR_INTEGRANTE : string = 'Deseas borrar a: ';
    public static readonly EXITO_ELIMINAR_INTEGRANTE : string = 'Eliminado exitosamente';

    //AlertaIntegrante
    public static readonly NO_EDITAR_ALERTA_INTEGRANTE : string = 'No puedes modificar';
    public static readonly SUBTITULO_ALERTA_INTEGRANTE: string = 'No eres el lider del grupo';
    public static readonly OK_ALERTA_INTEGRANTE : string = 'Esta bien';    

    //Mensajes Toast
    public static readonly ERROR : string = 'Algo ha salido mal :(';
    public static readonly AGREGAR_MENSAJE : string = 'Agregar mensaje';
    public static readonly PETICION_ELIMINADA : string = 'Peticion eliminada';
    public static readonly GRUPO_EXITOSO : string = 'Grupo exitoso';
    public static readonly MODIFICAR_EXITOSO : string = 'Modificado exitosamente';
    
}