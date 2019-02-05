import { AnimalesModule } from './animales.module';

describe('AnimalesModule', () => {
  let animalesModule: AnimalesModule;

  beforeEach(() => {
    animalesModule = new AnimalesModule();
  });

  it('should create an instance', () => {
    expect(animalesModule).toBeTruthy();
  });
});
