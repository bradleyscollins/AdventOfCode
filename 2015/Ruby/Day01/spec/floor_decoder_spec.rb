require 'rspec'
require 'floor_decoder'

RSpec.describe FloorDecoder do
  describe "#decode" do
    specify { expect(subject.decode '(())').to    eq  0 }
    specify { expect(subject.decode '()()').to    eq  0 }
    specify { expect(subject.decode '(((').to     eq  3 }
    specify { expect(subject.decode '(()(()(').to eq  3 }
    specify { expect(subject.decode '))(((((').to eq  3 }
    specify { expect(subject.decode '())').to     eq -1 }
    specify { expect(subject.decode '))(').to     eq -1 }
    specify { expect(subject.decode ')))').to     eq -3 }
    specify { expect(subject.decode ')())())').to eq -3 }
  end
end
