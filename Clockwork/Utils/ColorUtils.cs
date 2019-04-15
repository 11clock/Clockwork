using System;
using Microsoft.Xna.Framework;

namespace Clockwork.Utils
{
	public static class ColorUtils
	{
		/// <summary>
		/// Make Color from HSV values
		/// h is from 0-360
		/// s,v values are 0-1
		/// Based upon http://ilab.usc.edu/wiki/index.php/HSV_And_H2SV_Color_Space#HSV_Transformation_C_.2F_C.2B.2B_Code_2
		/// </summary>
		public static Color GetColorHsv(float h, float s, float v)
		{
			// ######################################################################
			// T. Nathan Mundhenk
			// mundhenk@usc.edu
			// C/C++ Macro HSV to RGB

			int r;
			int g;
			int b;

			float H = h;
			while (H < 0)
			{
				H += 360;
			}

			;
			while (H >= 360)
			{
				H -= 360;
			}

			;
			float R, G, B;
			if (v <= 0)
			{
				R = G = B = 0;
			}
			else if (s <= 0)
			{
				R = G = B = v;
			}
			else
			{
				float hf = H / 60f;
				int i = (int) Math.Floor(hf);
				float f = hf - i;
				float pv = v * (1 - s);
				float qv = v * (1 - s * f);
				float tv = v * (1 - s * (1 - f));
				switch (i)
				{
					// Red is the dominant color

					case 0:
						R = v;
						G = tv;
						B = pv;
						break;

					// Green is the dominant color

					case 1:
						R = qv;
						G = v;
						B = pv;
						break;
					case 2:
						R = pv;
						G = v;
						B = tv;
						break;

					// Blue is the dominant color

					case 3:
						R = pv;
						G = qv;
						B = v;
						break;
					case 4:
						R = tv;
						G = pv;
						B = v;
						break;

					// Red is the dominant color

					case 5:
						R = v;
						G = pv;
						B = qv;
						break;

					// Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

					case 6:
						R = v;
						G = tv;
						B = pv;
						break;
					case -1:
						R = v;
						G = pv;
						B = qv;
						break;

					default:
						R = G = B = v; // Just pretend its black/white
						break;
				}
			}

			r = Clamp((int) (R * 255.0));
			g = Clamp((int) (G * 255.0));
			b = Clamp((int) (B * 255.0));

			return new Color(r, g, b);
		}

		/// <summary>
		/// Clamp a value to 0-255
		/// </summary>
		private static int Clamp(int i)
		{
			if (i < 0) return 0;
			if (i > 255) return 255;
			return i;
		}
	}
}