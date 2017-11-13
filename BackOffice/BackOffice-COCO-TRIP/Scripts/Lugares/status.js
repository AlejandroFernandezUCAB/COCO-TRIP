function cambiarEstado(id)
{
	console.log("id del status a modificar " + id);
    if ( document.getElementById(id).classList.contains('active') ){
    	document.getElementById(id).classList.remove('active');
    	document.getElementById(id).classList.add('inactive');
    }

    else if ( document.getElementById(id).classList.contains('inactive') ){
    	document.getElementById(id).classList.remove('inactive');
    	document.getElementById(id).classList.add('active');
    }
}