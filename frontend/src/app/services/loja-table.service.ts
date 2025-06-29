import { Injectable } from "@angular/core";
import { IColunaTabela } from "../shared/components/lista-resumida/lista-resumida.component";

@Injectable({
    providedIn: 'root'
})
export class LojaTableService {
    criarColunasTabelaLojaResumida(): IColunaTabela[] {
        return  [
            { key: 'id', label: 'Id' },
            { key: 'nome', label: 'Nome' },
            { key: 'site', label: 'Site' }
        ]
    }
    
    criarColunasTabelaLojaCompleta(): IColunaTabela[] {
        return  [
            { key: 'id', label: 'Id' },
            { key: 'nome', label: 'Nome' },
            { key: 'site', label: 'Site' },
            { key: 'parceira', label: 'Parceira' },
            { key: 'ativa', label: 'Ativa' }
        ]
    }
}