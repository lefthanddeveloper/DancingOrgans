using CSCore.DSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ISpectrumProvider
{
	bool GetFftData(float[] fftBuffer, object context);
	int GetFftBandIndex(float frequency);
}

public class BasicSpectrumProvider : FftProviderEx, ISpectrumProvider
{
	private readonly int _sampleRate;
	private readonly List<object> _contexts = new List<object>();

	public BasicSpectrumProvider(int channels, int sampleRate, FftSize fftSize, WindowFunction windowFunction) : base(channels, fftSize)
	{
		if (sampleRate <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(sampleRate));
		}

		_sampleRate = sampleRate;
		WindowFunction = windowFunction;
	}

	public int GetFftBandIndex(float frequency)
	{
		int fftSize = (int)Fftsize;
		double f = _sampleRate / 2.0f;
		return (int)((frequency / f) * (fftSize / 2));
	}

	public bool GetFftData(float[] fftResultBuffer, object context)
	{
		if (_contexts.Contains(context)) return false;

		_contexts.Add(context);
		GetFftData(fftResultBuffer);
		return true;
	}

	public override void Add(float left, float right)
	{
		base.Add(left, right);
		_contexts.Clear();
	}

	public override void Add(float[] samples, int count)
	{
		base.Add(samples, count);
		if (count > 0)
		{
			_contexts.Clear();
		}
	}
}
