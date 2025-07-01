import { TestBed } from '@angular/core/testing';

import { TimeMapperService } from './time-mapper.service';

describe('TimeMapperService', () => {
  let service: TimeMapperService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TimeMapperService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
