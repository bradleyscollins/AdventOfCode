# frozen_string_literal: true

require 'rspec'
require 'brightness_grid'

RSpec.describe BrightnessGrid do
  describe "#apply" do
    context "after applying 'turn on 0,0 through 0,0'" do
      it "should have a total brightness of 1" do
        instruction = Instruction.parse('turn on 0,0 through 0,0')
        subject.apply instruction
        expect(subject.total_brightness).to eq 1
      end
    end
    context "after applying 'toggle 0,0 through 999,999'" do
      it "should have a total brightness of 2,000,000" do
        instruction = Instruction.parse('toggle 0,0 through 999,999')
        subject.apply instruction
        expect(subject.total_brightness).to eq 2_000_000
      end
    end
  end
end
