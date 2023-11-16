import logo from './logo.svg';
import './App.css';
import { UserProvider } from './context/UserContext';
import { BrowserRouter,Routes,Route } from 'react-router-dom';
import HomePage from './pages/HomePage';
import Login from './pages/Login';
import Layout from './components/Layout';
import EventPage from './pages/EventPage';
import EventVotePage from './pages/EventVotePage';
import EventResultsPage from './pages/EventResultsPage';
import TrackVotingPage from './pages/TrackVotingPage';
import MyEventsPage from './pages/MyEventsPage';
function App() {
  return (
    <UserProvider>
   <BrowserRouter>
   <Routes>
    <Route path="/" element={<Layout/>}>
    <Route index element={<Login/>}/>
    <Route path="/homePage"element={<HomePage/>}/>
    <Route path="/eventPage"element={<EventPage/>}/>
    <Route path="/eventVotePage/:id"element={<EventVotePage/>}/>
    <Route path="/eventResultsPage/:id"element={<EventResultsPage/>}/>
    <Route path="/trackVotingPage/:id"element={<TrackVotingPage/>}/>
    <Route path="/myEvents"element={<MyEventsPage/>}/>
    </Route>
   </Routes>
   </BrowserRouter>
   </UserProvider>
  );
}

export default App;
