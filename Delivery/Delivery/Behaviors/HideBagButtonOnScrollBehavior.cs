using Xamarin.Forms;

namespace Delivery.Behaviors
{
    public class HideBagButtonOnScrollBehavior : Behavior<ScrollView>
    {
        protected override void OnAttachedTo(ScrollView scrollView)
        {
            base.OnAttachedTo(scrollView);
            scrollView.Scrolled += OnScrollViewScrolled;
        }

        protected override void OnDetachingFrom(ScrollView scrollView)
        {
            base.OnDetachingFrom(scrollView);
            scrollView.Scrolled -= OnScrollViewScrolled;
        }

        private void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
        {
            var scrollView = (ScrollView)sender;
            var secondPrimartGridRow = scrollView.FindByName<RowDefinition>("secondPrimartGridRow");
            var bagButton = scrollView.FindByName<Button>("bagButton");

            if (e.ScrollY + scrollView.Height >= scrollView.ContentSize.Height - 20)
            {
                // ScrollView chegou ao final, oculta o botão
                //secondPrimartGridRow.Height = 80;
                bagButton.IsVisible = false;
            }
            else
            {
                // ScrollView não está no final, mostra o botão
                //secondPrimartGridRow.Height = 0;
                bagButton.IsVisible = true;
            }
        }
    }
}
