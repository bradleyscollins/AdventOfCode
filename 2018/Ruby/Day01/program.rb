require_relative 'frequency_summer'
require_relative 'duplicate_frequency_finder'

class App
  def load_input(path)
    IO.readlines(path).map(&:strip).reject(&:empty?).map(&:to_i)
  end

  def run
    input = load_input('./input.txt')
    
    result1 = FrequencySummer.new.calc(input)
    puts "Result 1: #{result1}"

    result2 = DuplicateFrequencyFinder.new.calc(input)
    puts "Result 2: #{result2}"
  end
end

app = App.new
app.run
