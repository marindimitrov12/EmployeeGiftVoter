import {useState,useEffect} from 'react'
import { getAllGifts } from '../services/ClientService'
import Gift from '../components/Gift';
import { useParams } from 'react-router-dom';
export default function EventVotePage(){
    const [gifts,setGifts]=useState(null);
    const {id}=useParams();
    useEffect(()=>{
      getAllGifts()
      .then((res)=>{
        console.log(res);
        setGifts(res);
      })
    },[]);

    return(<>
     <section className="py-5">
     <h1 className="headline">Vote</h1>
            <div className="container px-4 px-lg-5 mt-5">
                <div className="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center">
                   
                      
                    {gifts===null?<h1>Loading...</h1>:gifts.map(g=><Gift 
                    Name={g.name}
                    Id={g.id}
                    EventId={id}
                    ImgUrl={g.imgUrl}
                    key={g.id}/>)}
                   
                   
                </div>
            </div>
        </section>
    </>)
}