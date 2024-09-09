require 'rspec'
require 'basement_detector'

RSpec.describe BasementDetector do
  describe "#find_basement" do
    specify { expect(subject.find_basement ')').to     eq 1 }
    specify { expect(subject.find_basement '()())').to eq 5 }
  end
end
