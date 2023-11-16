import logo from './logo.svg';
import './App.css';
import { UserProvider } from './context/UserContext';
import { BrowserRouter,Routes,Route } from 'react-router-dom';
import HomePage from './pages/HomePage';
import Login from './pages/Login';
import Layout from './components/Layout';
import EventPage from './pages/EventPage';
function App() {
  return (
    <UserProvider>
   <BrowserRouter>
   <Routes>
    <Route path="/" element={<Layout/>}>
    <Route index element={<Login/>}/>
    <Route path="/homePage"element={<HomePage/>}/>
    <Route path="/eventPage"element={<EventPage/>}/>
    </Route>
   </Routes>
   </BrowserRouter>
   </UserProvider>
  );
}

export default App;
