import { useEffect, useState } from 'react';
import './App.css';

interface Album {
    albumTitle: string;
    userId: number;
    photoTitle: string;
    photoUrl: string;
}

function App() {
    const [albums, setAlbums] = useState<Album[]>();

    useEffect(() => {
        populateAlbums();
    }, []);

    const contents = albums === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>userId</th>
                    <th>albumTitle</th>
                    <th>photoTitle</th>
                    <th>photoUrl</th>
                </tr>
            </thead>
            <tbody>
                {albums.map(album =>
                    <tr key={album.userId}>
                        <td>{album.userId}</td>
                        <td>{album.albumTitle}</td>
                        <td>{album.photoTitle}</td>
                        <td>{album.photoUrl}</td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">album3 </h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );

    async function populateAlbums() {
        const response = await fetch('https://localhost:7184/api/Album?userId=1');
         
        const data = await response.json();
        setAlbums(data);
    }
}

export default App;