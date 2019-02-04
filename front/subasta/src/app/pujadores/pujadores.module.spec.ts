import { PujadoresModule } from './pujadores.module';

describe('PujadoresModule', () => {
  let pujadoresModule: PujadoresModule;

  beforeEach(() => {
    pujadoresModule = new PujadoresModule();
  });

  it('should create an instance', () => {
    expect(pujadoresModule).toBeTruthy();
  });
});
