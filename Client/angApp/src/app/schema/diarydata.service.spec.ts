import { TestBed } from '@angular/core/testing';

import { DiarydataService } from './diarydata.service';

describe('DiarydataService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DiarydataService = TestBed.get(DiarydataService);
    expect(service).toBeTruthy();
  });
});
