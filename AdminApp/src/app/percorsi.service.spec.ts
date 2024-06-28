import { TestBed } from '@angular/core/testing';

import { PercorsiService } from './percorsi.service';

describe('PercorsiService', () => {
  let service: PercorsiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PercorsiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
