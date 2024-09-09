# frozen_string_literal: true

require 'rspec'
require 'grid'

RSpec.describe Grid do
  describe "#apply" do
    context "after applying 'turn on 0,0 through 999,999'" do
      it "should have 1,000,000 lights on" do
        instruction = Instruction.parse('turn on 0,0 through 999,999')
        subject.apply instruction
        expect(subject.lights_lit).to eq 1_000_000
      end
    end
    context "after applying 'toggle 0,0 through 999,0'" do
      it "should have 1,000 lights on" do
        instruction = Instruction.parse('toggle 0,0 through 999,0')
        subject.apply instruction
        expect(subject.lights_lit).to eq 1000
      end
    end
    context "after applying 'turn off 499,499 through 500,500'" do
      it "should have 0 lights on" do
        instruction = Instruction.parse('turn off 499,499 through 500,500')
        subject.apply instruction
        expect(subject.lights_lit).to eq 0
      end
    end
  end
end
