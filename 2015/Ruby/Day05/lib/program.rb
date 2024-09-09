# frozen_string_literal: true

require_relative 'string_refinements'

class App
  using StringRefinements

  def run
    strings = ARGF.readlines.map(&:strip)

    puts "How many strings are nice? #{strings.count(&:nice?)}"
    puts "How many strings are nice under these new rules? #{strings.count(&:actually_nice?)}"
  end
end

App.new.run
