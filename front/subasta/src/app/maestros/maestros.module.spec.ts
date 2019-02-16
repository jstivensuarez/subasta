import { MaestrosModule } from './maestros.module';

describe('MaestrosModule', () => {
  let maestrosModule: MaestrosModule;

  beforeEach(() => {
    maestrosModule = new MaestrosModule();
  });

  it('should create an instance', () => {
    expect(maestrosModule).toBeTruthy();
  });
});
