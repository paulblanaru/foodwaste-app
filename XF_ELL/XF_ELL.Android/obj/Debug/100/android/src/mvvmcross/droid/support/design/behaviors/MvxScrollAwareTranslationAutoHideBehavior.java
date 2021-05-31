package mvvmcross.droid.support.design.behaviors;


public class MvxScrollAwareTranslationAutoHideBehavior
	extends androidx.coordinatorlayout.widget.CoordinatorLayout.Behavior
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onStartNestedScroll:(Landroidx/coordinatorlayout/widget/CoordinatorLayout;Landroid/view/View;Landroid/view/View;Landroid/view/View;II)Z:GetOnStartNestedScroll_Landroidx_coordinatorlayout_widget_CoordinatorLayout_Landroid_view_View_Landroid_view_View_Landroid_view_View_IIHandler\n" +
			"n_onNestedScroll:(Landroidx/coordinatorlayout/widget/CoordinatorLayout;Landroid/view/View;Landroid/view/View;IIIII)V:GetOnNestedScroll_Landroidx_coordinatorlayout_widget_CoordinatorLayout_Landroid_view_View_Landroid_view_View_IIIIIHandler\n" +
			"";
		mono.android.Runtime.register ("MvvmCross.Droid.Support.Design.Behaviors.MvxScrollAwareTranslationHideBottomBarBehavior, MvvmCross.Droid.Support.Design", MvxScrollAwareTranslationAutoHideBehavior.class, __md_methods);
	}


	public MvxScrollAwareTranslationAutoHideBehavior ()
	{
		super ();
		if (getClass () == MvxScrollAwareTranslationAutoHideBehavior.class)
			mono.android.TypeManager.Activate ("MvvmCross.Droid.Support.Design.Behaviors.MvxScrollAwareTranslationHideBottomBarBehavior, MvvmCross.Droid.Support.Design", "", this, new java.lang.Object[] {  });
	}


	public MvxScrollAwareTranslationAutoHideBehavior (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == MvxScrollAwareTranslationAutoHideBehavior.class)
			mono.android.TypeManager.Activate ("MvvmCross.Droid.Support.Design.Behaviors.MvxScrollAwareTranslationHideBottomBarBehavior, MvvmCross.Droid.Support.Design", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public boolean onStartNestedScroll (androidx.coordinatorlayout.widget.CoordinatorLayout p0, android.view.View p1, android.view.View p2, android.view.View p3, int p4, int p5)
	{
		return n_onStartNestedScroll (p0, p1, p2, p3, p4, p5);
	}

	private native boolean n_onStartNestedScroll (androidx.coordinatorlayout.widget.CoordinatorLayout p0, android.view.View p1, android.view.View p2, android.view.View p3, int p4, int p5);


	public void onNestedScroll (androidx.coordinatorlayout.widget.CoordinatorLayout p0, android.view.View p1, android.view.View p2, int p3, int p4, int p5, int p6, int p7)
	{
		n_onNestedScroll (p0, p1, p2, p3, p4, p5, p6, p7);
	}

	private native void n_onNestedScroll (androidx.coordinatorlayout.widget.CoordinatorLayout p0, android.view.View p1, android.view.View p2, int p3, int p4, int p5, int p6, int p7);

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
