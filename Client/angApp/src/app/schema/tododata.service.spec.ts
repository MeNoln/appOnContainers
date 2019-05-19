import { TestBed } from '@angular/core/testing';

import { TododataService } from './tododata.service';

describe('TododataService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TododataService = TestBed.get(TododataService);
    expect(service).toBeTruthy();
  });
});
