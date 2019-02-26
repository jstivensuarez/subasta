
export const urlBase = 'https://subastaservice.azurewebsites.net';
export const urlBaseApi = 'https://subastaservice.azurewebsites.net/api';
export const urlBaseImage = 'https://subastaservice.azurewebsites.net';
export const local = 'https://subasta-front.azurewebsites.net';

export const environment = {
  production: false,
  endpointTipoDocumentos: urlBaseApi+'/tipoDocumentos',
  endpointDepartamentos: urlBaseApi+'/departamentos',
  endpointMunicipios: urlBaseApi+'/municipios',
  endpointClientes: urlBaseApi+'/Clientes',
  endpointEvento: urlBaseApi+'/eventos',
  endpointSubasta: urlBaseApi+'/subastas',
  endpointLogin: urlBaseApi+'/login',
  endpointLote: urlBaseApi+'/lotes',
  endpointCategoria: urlBaseApi+'/categorias',
  endpointRaza: urlBaseApi+'/razas',
  endpointSexo: urlBaseApi+'/sexos',
  endpointAnimal: urlBaseApi+'/animales',
  endpointPujador: urlBaseApi+'/pujadores',
  endpointSolicitud: urlBaseApi+'/solicitudes',
  endpointClasificacion: urlBaseApi+'/clasificaciones',
  endpointUsuario: urlBaseApi+'/usuarios',
  endpointPuja: urlBaseApi+'/pujas',
  endpointSignal: urlBase+'/notificacion',
  
  imageLotesUrl: urlBaseImage+'/images/LOTES/',
  imageAnimalesUrl: urlBaseImage+'/images/ANIMALES/',
  imageUrl: urlBaseImage+'/images/',
};
