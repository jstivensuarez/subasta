import { Subasta } from "./subasta";
import { Cliente } from "./cliente";

export class Solicitud{
    
    solicitudId: number;
    estado: string;
    subastaId: number;
    subasta: Subasta;
    clienteId: number;
    cliente: Cliente;

    constructor(){}
}