import { LotesModule } from './lotes.module';

describe('LotesModule', () => {
  let lotesModule: LotesModule;

  beforeEach(() => {
    lotesModule = new LotesModule();
  });

  it('should create an instance', () => {
    expect(lotesModule).toBeTruthy();
  });
});
