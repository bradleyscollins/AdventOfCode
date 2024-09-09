# frozen_string_literal: true

require_relative 'floor_decoder'
require_relative 'basement_detector'

class App
  def run
    decoder = FloorDecoder.new
    detector = BasementDetector.new
    code = ARGF.read.strip

    floor = decoder.decode code
    puts "Santa should go to floor #{floor}"

    position = detector.find_basement code
    puts "Santa arrived at the basement at position #{position}"
  end
end

App.new.run
