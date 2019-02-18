
export const urlBase = 'https://subastaservice.azurewebsites.net/api';
export const urlBaseImage = 'https://subastaservice.azurewebsites.net';

export const environment = {
  production: false,
  endpointTipoDocumentos: urlBase+'/tipoDocumentos',
  endpointDepartamentos: urlBase+'/departamentos',
  endpointMunicipios: urlBase+'/municipios',
  endpointClientes: urlBase+'/Clientes',
  endpointEvento: urlBase+'/eventos',
  endpointSubasta: urlBase+'/subastas',
  endpointLogin: urlBase+'/login',
  endpointLote: urlBase+'/lotes',
  endpointCategoria: urlBase+'/categorias',
  endpointRaza: urlBase+'/razas',
  endpointSexo: urlBase+'/sexos',
  endpointAnimal: urlBase+'/animales',
  endpointPujador: urlBase+'/pujadores',
  endpointSolicitud: urlBase+'/solicitudes',
  endpointClasificacion: urlBase+'/clasificaciones',

  imageLotesUrl: urlBaseImage+'/images/LOTES/',
  imageAnimalesUrl: urlBaseImage+'/images/ANIMALES/',
  imageUrl: urlBaseImage+'/images/',
};
