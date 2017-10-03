package md52afd1bc2b0438d27113fc6044af0fe61;


public class CustomNavigationPageRenderer
	extends md5270abb39e60627f0f200893b490a1ade.NavigationPageRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onViewAdded:(Landroid/view/View;)V:GetOnViewAdded_Landroid_view_View_Handler\n" +
			"";
		mono.android.Runtime.register ("PrismAria.Droid.CustomRenderers.CustomNavigationPageRenderer, PrismAria.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CustomNavigationPageRenderer.class, __md_methods);
	}


	public CustomNavigationPageRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == CustomNavigationPageRenderer.class)
			mono.android.TypeManager.Activate ("PrismAria.Droid.CustomRenderers.CustomNavigationPageRenderer, PrismAria.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public CustomNavigationPageRenderer (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == CustomNavigationPageRenderer.class)
			mono.android.TypeManager.Activate ("PrismAria.Droid.CustomRenderers.CustomNavigationPageRenderer, PrismAria.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public CustomNavigationPageRenderer (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == CustomNavigationPageRenderer.class)
			mono.android.TypeManager.Activate ("PrismAria.Droid.CustomRenderers.CustomNavigationPageRenderer, PrismAria.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onViewAdded (android.view.View p0)
	{
		n_onViewAdded (p0);
	}

	private native void n_onViewAdded (android.view.View p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
