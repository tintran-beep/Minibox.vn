import AppRouter from "@routes/AppRouter";
import { Provider } from "react-redux";
import store from "@stores/index";

function App() {
  return (
    <Provider store={store}>
      <AppRouter/>
    </Provider>
  );
}

export default App;
