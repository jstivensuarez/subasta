import { Rol } from "./rol";

export class Usuario {
    usuarioId: string;
    nombre: string;
    correo: string;
    clave: string;
    claveChange: string;
    rolId: number;
    rol: Rol;
    ingreso: string;
}

