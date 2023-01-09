import { ShowMedicinesPipe } from './show-medicines.pipe';

describe('ShowMedicinesPipe', () => {
  it('create an instance', () => {
    const pipe = new ShowMedicinesPipe();
    expect(pipe).toBeTruthy();
  });
});
