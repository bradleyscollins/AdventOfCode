require 'set'

class DuplicateFrequencyFinder
  def calc(frequency_changes)
    last_frequency = 0
    previous_frequencies = [last_frequency].to_set
    infinite(frequency_changes).lazy.each do |change|
      next_frequency = last_frequency + change
      if previous_frequencies.include? next_frequency
        return next_frequency
      else
        previous_frequencies << next_frequency
        last_frequency = next_frequency
      end
    end
  end

  private
    def infinite(frequency_changes)
      Enumerator.new do |yielder|
        len = frequency_changes.length
        i = 0
        loop do
          yielder << frequency_changes[i]
          i = (i + 1) % len
        end
      end
    end
end
