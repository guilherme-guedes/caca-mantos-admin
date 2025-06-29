import { Time } from './time';

describe('Time', () => {
  it('should create an instance', () => {
    const time: Time ={
      id: '1',
      nome: 'Fluminense',
      identificador: 'fluminense-rj',
      nomeBusca: 'Fluminense',
      destaque: true,
      ativo: true,
      principal: true,
      termos: [],
      homonimos: ['Fluminense de Feira', 'Fluminense de Joinville']
    };

    expect(time.id).toBe('1');
    expect(time.nome).toBe('Fluminense');
    expect(time.identificador).toBe('fluminense-rj');
    expect(time.nomeBusca).toBe('Fluminense');
    expect(time.destaque).toBeTrue();
    expect(time.ativo).toBeTrue();
    expect(time.principal).toBeTrue();
    expect(time.termos).toEqual([]);
    expect(time.homonimos).toContain('Fluminense de Feira');
  });
});
