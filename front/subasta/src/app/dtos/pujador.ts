import { Lote } from "./lote";
import { Cliente } from "./cliente";

export class Pujador {

    pujadorId: number;
    loteId: number;
    lote: Lote;
    clienteId: number;
    cliente: Cliente;
    numeroConsignacion: number;
    banco: string;
    valorConsignacion: number;
    estado: string;
}