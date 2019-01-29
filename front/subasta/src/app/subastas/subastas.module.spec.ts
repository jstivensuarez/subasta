import { SubastasModule } from './subastas.module';

describe('SubastasModule', () => {
  let subastasModule: SubastasModule;

  beforeEach(() => {
    subastasModule = new SubastasModule();
  });

  it('should create an instance', () => {
    expect(subastasModule).toBeTruthy();
  });
});
