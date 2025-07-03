import { TestBed } from '@angular/core/testing';

import { LojaMapperService } from './loja-mapper.service';

describe('LojaMapperService', () => {
  let service: LojaMapperService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LojaMapperService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
